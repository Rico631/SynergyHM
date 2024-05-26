
from utils import *


class Helicopter:
    def __init__(self, w, h) -> None:
        self.w = w
        self.h = h
        rc = randcell(w, h)
        rx, ry = rc[0], rc[1]
        self.x = rx
        self.y = ry
        self.tank = 0
        self.mxtank = 1
        self.score = 0
        self.lifes = 2

    def move(self, dx, dy):
        nx, ny = dx + self.x, dy + self.y
        if nx >= 0 and ny >= 0 and nx < self.h and ny < self.w:
            self.x, self.y = nx, ny

    def print_status(self):
        print(f'🪣  {self.tank}/{self.mxtank} | ❤️  {self.lifes} | 🏆 {self.score}')
