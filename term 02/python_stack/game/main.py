from pynput import keyboard
from pynput.keyboard import Key
import time
import os
import json
from clouds import Clouds
from map import Map
from helicopter import Helicopter as Heli

TICK_SLEEP = 0.05
TREE_UPDATE = 200
FIRE_UPDATE = 50
CLOUD_UPDATE = 100
MAP_W, MAP_H = 20, 10
# MOVES = {'w': (-1, 0), 'd': (0, 1), 's': (1, 0), 'a': (0, -1)}
MOVES = {Key.up: (-1, 0), Key.right: (0, 1),
         Key.down: (1, 0), Key.left: (0, -1)}

field = Map(MAP_W, MAP_H)
heli = Heli(MAP_W, MAP_H)
clouds = Clouds(MAP_W, MAP_H)
tick = 1

# f - save
# g - load


def on_release(key: Key):
    global field, heli, clouds, tick
    if hasattr(key, 'char'):
        c = key.char.lower()
        # Save
        if c == 'f':
            data = {'heli': heli.export_data(),
                    'clouds': clouds.export_data(),
                    'field': field.export_data(),
                    'tick': tick}
            with open('level.json', 'w') as lvl:
                json.dump(data, lvl)
        # Load
        elif c == 'g':
            with open('level.json', 'r') as lvl:
                data = json.load(lvl)
                heli.import_data(data['heli'])
                tick = data['tick'] or 1
                clouds.import_data(data['clouds'])
                field.import_data(data['field'])

    if key in MOVES.keys():
        dx, dy = MOVES[key][0], MOVES[key][1]
        heli.move(dx, dy)

    # c = key.char.lower()
    # if c in MOVES.keys():
    #     dx, dy = MOVES[c][0], MOVES[c][1]
    #     heli.move(dx, dy)


listener = keyboard.Listener(
    on_press=None,
    on_release=on_release)
listener.start()


while True:
    os.system('cls')
    field.proccess_heli(heli, clouds)
    heli.print_status()
    field.print_map(heli, clouds)
    print(f'Tick: {tick}')
    tick += 1
    time.sleep(TICK_SLEEP)
    if tick % TREE_UPDATE == 0:
        field.generate_tree()
    if tick % FIRE_UPDATE == 0:
        field.update_fires()
    if tick % CLOUD_UPDATE == 0:
        clouds.update()
