import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
import switch as switch

WALL_SCALE_X = 0.5
WALL_SCALE_Y = 0.25

def create_heatmaps(all_dicts):
    i = 1
    for dicts in all_dicts:
        make_heatmaps(dicts, i)
        i += 1


def make_heatmaps(proband, i):
    img = plt.imread("background.png")
    img_small = plt.imread("background_small.png")
    x1, y1, b1 = create_single_map(proband[2][0])
    x2, y2, b2 = create_single_map(proband[2][1])
    x3, y3, b3 = create_single_map(proband[3][0])
    x4, y4, b4 = create_single_map(proband[3][1])
    fig, axs = plt.subplots(2, 2)
    fig.suptitle(f"Heatmaps Eye-Tracking Prob{i}", fontsize="11")
    plt.subplots_adjust(wspace=0.2)
    plt.subplots_adjust(hspace=0.2)

    axs[0, 0].plot(x1, y1, 'y', label='Gaze Data', linewidth=1, alpha=0.85)
    axs[0, 0].plot(b1[0], b1[1], 'ob', label='Button Positions', markersize=3, alpha=0.9)
    axs[0, 0].set_title('Eye-Tracking/Trigger Big', size=8)
    axs[0, 0].set_xlabel('Units', fontsize=6)
    axs[0, 0].set_ylabel('Units', fontsize=6)
    axs[0, 0].tick_params(axis='both', which='major', labelsize=6)
    axs[0, 0].legend(loc='upper left', fontsize=6, bbox_to_anchor=(-0.05, -0.15))
    axs[0, 0].imshow(img, extent=[-2.5, 2.5, 0, 2.5])

    axs[0, 1].plot(x2, y2, 'y', label='Gaze Data', linewidth=1, alpha=0.85)
    axs[0, 1].plot(b2[0], b2[1], 'ob', label='Button Positions', markersize=3, alpha=0.9)
    axs[0, 1].set_title('Eye-Tracking/Trigger Small', size=8)
    axs[0, 1].set_xlabel('Units', fontsize=6)
    axs[0, 1].set_ylabel('Units', fontsize=6)
    axs[0, 1].tick_params(axis='both', which='major', labelsize=6)
    axs[0, 1].legend(loc='upper left', fontsize=6, bbox_to_anchor=(-0.05, -0.15))
    axs[0, 1].imshow(img_small, extent=[-2.5, 2.5, 0, 2.5])

    axs[1, 0].plot(x3, y3, 'y', label='Gaze Data', linewidth=1, alpha=0.85)
    axs[1, 0].plot(b3[0], b3[1], 'ob', label='Button Positions', markersize=3, alpha=0.9)
    axs[1, 0].set_title('Eye-Tracking/Blink Big', size=8)
    axs[1, 0].set_xlabel('Units', fontsize=6)
    axs[1, 0].set_ylabel('Units', fontsize=6)
    axs[1, 0].tick_params(axis='both', which='major', labelsize=6)
    axs[1, 0].legend(loc='upper left', fontsize=6, bbox_to_anchor=(-0.05, -0.15))
    axs[1, 0].imshow(img, extent=[-2.5, 2.5, 0, 2.5])

    axs[1, 1].plot(x4, y4, 'y', label='Gaze Data', linewidth=1, alpha=0.85)
    axs[1, 1].plot(b4[0], b4[1], 'ob', label='Button Positions', markersize=3, alpha=0.9)
    axs[1, 1].set_title('Eye-Tracking/Blink Small', size=8)
    axs[1, 1].set_xlabel('Units', fontsize=6)
    axs[1, 1].set_ylabel('Units', fontsize=6)
    axs[1, 1].tick_params(axis='both', which='major', labelsize=6)
    axs[1, 1].legend(loc='upper left', fontsize=6, bbox_to_anchor=(-0.05, -0.15))
    axs[1, 1].imshow(img_small, extent=[-2.5, 2.5, 0, 2.5])

    plt.savefig(f"plot_heatmap_prob{i}.png", format='png', dpi=200)


def create_single_map(measurement):
    x_values = []
    y_values = []
    buttons = []
    button_positions = [[], []]
    for gazepoint in measurement['gazePoints']:
        split_str = gazepoint.split(', ')
        split_str[0] = split_str[0].replace('(', '')
        x_values.append(float(split_str[0].replace(',', '.')) * -1)
        y_values.append(float(split_str[1].replace(',', '.')))

    for action in measurement['action']:
        split_str = action.split(',')
        if int(split_str[1]) != -1:
            buttons.append(int(split_str[1]))
            get_button_position(int(split_str[1]), button_positions)
    return x_values, y_values, button_positions


def get_button_position(button, button_positions):
    a_x = (button - 1) % 4
    a_y = (button - 1) // 4
    b_x = 0
    b_y = 0
    if a_x == 0:
        b_x = -3
    elif a_x == 1:
        b_x = -1
    elif a_x == 2:
        b_x = 1
    elif a_x == 3:
        b_x = 3
    if a_y == 0:
        b_y = 2
    elif a_y == 1:
        b_y = 0
    elif a_y == 2:
        b_y = -2
    elif a_y == 3:
        b_y = -4
    button_positions[0].append(b_x * WALL_SCALE_X)
    button_positions[1].append((b_y + 5) * WALL_SCALE_Y)
