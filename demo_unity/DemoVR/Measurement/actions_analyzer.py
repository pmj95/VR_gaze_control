import re


def get_acts_per_sec(all_dicts, mean_times):
    act_counter_big = [[], [], [], []]
    act_counter_small = [[], [], [], []]
    for dicts in all_dicts:
        act_counter_big[0].append(len(dicts[0][0]["action"])/((dicts[0][0]["stopTime"] - dicts[0][0]["startTime"])/1000))
        act_counter_big[1].append(len(dicts[1][0]["action"])/((dicts[1][0]["stopTime"] - dicts[1][0]["startTime"])/1000))
        act_counter_big[2].append(len(dicts[2][0]["action"])/((dicts[2][0]["stopTime"] - dicts[2][0]["startTime"])/1000))
        act_counter_big[3].append(len(dicts[3][0]["action"])/((dicts[3][0]["stopTime"] - dicts[3][0]["startTime"])/1000))
        act_counter_small[0].append(len(dicts[0][1]["action"])/((dicts[0][1]["stopTime"] - dicts[0][1]["startTime"])/1000))
        act_counter_small[1].append(len(dicts[1][1]["action"])/((dicts[1][1]["stopTime"] - dicts[1][1]["startTime"])/1000))
        act_counter_small[2].append(len(dicts[2][1]["action"])/((dicts[2][1]["stopTime"] - dicts[2][1]["startTime"])/1000))
        act_counter_small[3].append(len(dicts[3][1]["action"])/((dicts[3][1]["stopTime"] - dicts[3][1]["startTime"])/1000))

    mean_act_big = []
    mean_act_small = []
    for act in act_counter_big:
        mean_act_big.append(sum(act) / len(act))

    for act in act_counter_small:
        mean_act_small.append(sum(act) / len(act))

    return act_counter_big, mean_act_big, act_counter_small, mean_act_small
