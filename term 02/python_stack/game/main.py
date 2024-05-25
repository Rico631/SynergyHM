from pynput import keyboard
import time
import os
from map import Map
from helicopter import Helicopter as Heli

TICK_SLEEP = 0.05
TREE_UPDATE = 50
FIRE_UPDATE = 50
MAP_W, MAP_H = 20, 10
MOVES = {'w': (-1, 0), 'd': (0, 1), 's': (1, 0), 'a': (0, -1)}

field = Map(MAP_W, MAP_H)
heli = Heli(MAP_W, MAP_H)


def on_release(key):
    global heli
    c = key.char.lower()
    if c in MOVES.keys():
        dx, dy = MOVES[c][0], MOVES[c][1]
        heli.move(dx, dy)


listener = keyboard.Listener(
    on_press=None,
    on_release=on_release)
listener.start()

tick = 1

while True:
    os.system('cls')
    print(f'Tick: {tick}')
    field.proccess_heli(heli)
    heli.print_status()
    field.print_map(heli)
    tick += 1
    time.sleep(TICK_SLEEP)
    if tick % TREE_UPDATE == 0:
        field.generate_tree()
    if tick % FIRE_UPDATE == 0:
        field.update_fires()
