import pandas as pd
from dict_getters import get_dicts
from time_analysis import get_mean_time_data

andre_dicts = get_dicts("./Andre")
clemens_dicts = get_dicts("./clemens")
joern_dicts = get_dicts("./joern")
juli_dicts = get_dicts("./juli")
all_dicts = [andre_dicts, clemens_dicts, joern_dicts, juli_dicts]

x = get_mean_time_data(all_dicts)
print(x[1])
print(x[3])


print(x)


