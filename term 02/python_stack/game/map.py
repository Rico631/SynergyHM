from utils import *

CELL_TYPES = '🟩🌲🌊🏥🏪⬛'


class Map:

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
        if self.check_bounds(cx, cy) and self.cells[cx][cy] == 0:
            self.cells[cx][cy] = 1

    def print_map(self):
        print(CELL_TYPES[-1] * (self.w + 2))
        for row in self.cells:
            print(CELL_TYPES[-1], end='')
            for cell in row:
                if cell >= 0 and cell < len(CELL_TYPES):
                    print(CELL_TYPES[cell], end='')
            print(CELL_TYPES[-1])
        print(CELL_TYPES[-1] * (self.w + 2))

    def check_bounds(self, x: int, y: int) -> bool:
        if x < 0 or y < 0 or x >= self.h or y >= self.w:
            return False
        return True

    def __init__(self, w: int, h: int) -> None:
        self.w = w
        self.h = h
        self.cells = [[0 for i in range(w)] for j in range(h)]
