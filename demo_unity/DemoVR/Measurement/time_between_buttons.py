import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
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
    avg_times = get_avrg_times(times)
    plot_avg_times(avg_times)
    pass


def get_avrg_times(times):
    avrg_times = [[[], []], [[], []], [[], []], [[], []], [[], []], [[], []], [[], []], [[], []]]
    for k in range(8):
        for dist in times[k]:
            if len(avrg_times[k][0]) == 0:
                avrg_times[k][0].append(dist)
                avrg_times[k][1].append((sum(times[k][dist]) / len(times[k][dist])) / 1000)
            else:
                i = 0
                while avrg_times[k][0][i] < dist:
                    i += 1
                    if i == len(avrg_times[k][0]):
                        break
                avrg_times[k][0].insert(i, dist)
                avrg_times[k][1].insert(i, (sum(times[k][dist])/len(times[k][dist]))/1000)
    return avrg_times


def plot_avg_times(times):
    data_labels = ['Laser/Trigger', 'Laser/Blink', 'Eye/Trigger', 'Eye/Blink']
    fig, axs = plt.subplots(2, 2)
    axs[0, 0].plot(times[0][0], times[0][1])
    axs[0, 0].plot(times[1][0], times[1][1])
    axs[0, 0].set_title('Laser/Trigger')
    axs[0, 1].plot(times[2][0], times[2][1])
    axs[0, 1].plot(times[3][0], times[3][1])
    axs[0, 1].set_title('Laser/Blink')
    axs[1, 0].plot(times[4][0], times[4][1])
    axs[1, 0].plot(times[5][0], times[5][1])
    axs[1, 0].set_title('Eye/Trigger')
    axs[1, 1].plot(times[6][0], times[6][1])
    axs[1, 1].plot(times[7][0], times[7][1])
    axs[1, 1].set_title('Eye/Blink')
    plt.savefig("plot_buttons.png", format='png', dpi=200)

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
    if dist in times_dict.keys():
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
