from clouds import Clouds
from utils import *
from helicopter import Helicopter

CELL_TYPES = '🟩🌲🌊🏥🏪🔥🚁🏥⛅🌦️⬛'
TREE_BONUS = 100
UPGRADE_COST = 300
LIFE_COST = 300


class Map:

    def __init__(self, w: int, h: int) -> None:
        self.w = w
        self.h = h
        self.cells = [[0 for i in range(w)] for j in range(h)]
        self.generate_forest(3, 10)
        self.generate_river(10)
        self.generate_river(10)
        self.generate_river(10)
        self.generate_upgrade_shop()
        self.generate_hospital()

    def check_bounds(self, x: int, y: int) -> bool:
        if x < 0 or y < 0 or x >= self.h or y >= self.w:
            return False
        return True

    def print_map(self, heli: Helicopter, clouds: Clouds):
        print(CELL_TYPES[-1] * (self.w + 2))
        for ri in range(self.h):
            print(CELL_TYPES[-1], end='')
            for ci in range(self.w):
                cell = self.cells[ri][ci]
                if clouds.cells[ri][ci] == 1:
                    print(CELL_TYPES[8], end='')
                elif clouds.cells[ri][ci] == 2:
                    print(CELL_TYPES[9], end='')
                elif heli.x == ri and heli.y == ci:
                    print(CELL_TYPES[6], end='')
                elif cell >= 0 and cell < len(CELL_TYPES):
                    print(CELL_TYPES[cell], end='')
            print(CELL_TYPES[-1])
        print(CELL_TYPES[-1] * (self.w + 2))

    def generate_river(self, l):
        rx, ry = randcell(self.w, self.h)
        self.cells[rx][ry] = 2
        while l > 0:
            rx2, ry2 = randcell2(rx, ry)
            if self.check_bounds(rx2, ry2):
                self.cells[rx2][ry2] = 2
                rx, ry = rx2, ry2
                l -= 1

    def generate_forest(self, r, mxr):
        for ri in range(self.h):
            for ci in range(self.w):
                if randbool(r, mxr):
                    self.cells[ri][ci] = 1

    def generate_tree(self):
        cx, cy = randcell(self.w, self.h)
        if self.cells[cx][cy] == 0:
            self.cells[cx][cy] = 1

    def add_fire(self):
        cx, cy = randcell(self.w, self.h)
        if self.cells[cx][cy] == 1:
            self.cells[cx][cy] = 5

    def update_fires(self):
        for ri in range(self.h):
            for ci in range(self.w):
                cell = self.cells[ri][ci]
                if cell == 5:
                    self.cells[ri][ci] = 0
        for i in range(10):
            self.add_fire()

    def generate_upgrade_shop(self):
        cx, cy = randcell(self.w, self.h)
        self.cells[cx][cy] = 4

    def generate_hospital(self):
        cx, cy = randcell(self.w, self.h)
        if self.cells[cx][cy] != 4:
            self.cells[cx][cy] = 7
        else:
            self.generate_hospital()

    def proccess_heli(self, heli: Helicopter, clouds: Clouds):
        c = self.cells[heli.x][heli.y]
        d = clouds.cells[heli.x][heli.y]
        if c == 2:
            heli.tank = heli.mxtank
        if c == 5 and heli.tank > 0:
            heli.tank -= 1
            heli.score += TREE_BONUS
            self.cells[heli.x][heli.y] = 1
        if c == 4 and heli.score >= UPGRADE_COST:
            heli.mxtank += 1
            heli.score -= UPGRADE_COST
        if c == 4 and heli.score >= LIFE_COST:
            heli.lifes += 1
            heli.score -= LIFE_COST
        if d == 2:
            heli.lifes -= 1
            if heli.lifes == 0:
                print(f'GAME OVER, YOUR SCORE IS {heli.score}')
                exit(0)
