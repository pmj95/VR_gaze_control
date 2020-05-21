import pandas as pd
from dict_getters import get_dicts
from time_analysis import get_mean_time_data
from accuracy_analysis import get_accuracy
from actions_analyzer import get_acts_per_sec

andre_dicts = get_dicts("./Andre")
clemens_dicts = get_dicts("./clemens")
joern_dicts = get_dicts("./joern")
juli_dicts = get_dicts("./juli")
all_dicts = [andre_dicts, clemens_dicts, joern_dicts, juli_dicts]

x = get_mean_time_data(all_dicts)
y = get_accuracy(all_dicts)
z = get_acts_per_sec(all_dicts, (x[1], x[3]))
print(x[1])
print(x[3])
print(y[1])
print(y[3])
print(z[1])
print(z[3])



