import numpy as np
import pandas as pd
import re
import math
DIST_X = 2.5
DIST_Y = 1.5


def calc_time_between(data):
    times = [dict(), dict(), dict(), dict(),
             dict(), dict(), dict(), dict()]
    for dicts in data:
        update_dicts(times, dicts)
    print(times)
    pass


def update_dicts(times, dicts):
    i = 0
    j = 0
    for k in range(8):
        add_values(times[k], dicts[i][j])
        if j == 1:
            j = 0
            i += 1
        else:
            j = 1


def add_values(times_dict, file):
    j = 0
    reg = re.compile(r'True')
    previous = (0, 0)
    for i in range(4):
        if i == 0:
            while True:
                if reg.search(file['action'][j]):
                    str_split = file['action'][j].split(",")
                    previous = (int(str_split[0]), int(str_split[1]))
                    j += 1
                    break
                else:
                    j += 1
        while True:
            if reg.search(file['action'][j]):
                str_split = file['action'][j].split(",")
                current = (int(str_split[0]), int(str_split[1]))
                update_single_dict(previous, current, times_dict)
                previous = (int(str_split[0]), int(str_split[1]))
                j += 1
                break
            else:
                j += 1


def update_single_dict(previous, current, times_dict):
    time_diff = current[0] - previous[0]
    dist = get_distance_between_buttons(current[1], previous[1])
    if time_diff in times_dict:
        times_dict[dist].append(time_diff)
    else:
        times_dict[dist] = [time_diff]

def get_distance_between_buttons(a, b):
    a = a - 1
    b = b - 1
    a_x = a % 4
    a_y = a // 4
    b_x = b % 4
    b_y = b // 4
    diff_x = abs(a_x - b_x) * DIST_X
    diff_y = abs(a_y - b_y) * DIST_Y
    dist = math.hypot(diff_x, diff_y)
    return dist
