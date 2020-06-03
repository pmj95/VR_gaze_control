import pandas as pd
import numpy as np
from dict_getters import get_dicts
from heatmaps import create_heatmaps
from time_analysis import *
from accuracy_analysis import *
from actions_analyzer import get_acts_per_sec
from time_between_buttons import calc_time_between

andre_dicts = get_dicts("./Andre")
clemens_dicts = get_dicts("./clemens")
joern_dicts = get_dicts("./joern")
juli_dicts = get_dicts("./juli")
all_dicts = [andre_dicts, clemens_dicts, joern_dicts, juli_dicts]

x = get_mean_time_data(all_dicts)
y = get_accuracy(all_dicts)
z = get_acts_per_sec(all_dicts, (x[1], x[3]))
create_bar_graph(x)
create_bar_graph_accuracy(y)
create_bar_graph_increase_time(x)
# calc_time_between(all_dicts)
# create_heatmaps(all_dicts)
print(x[0])
print(x[1])
print(x[2])
print(x[3])
print(y[0])
print(y[1])
print(y[2])
print(y[3])



