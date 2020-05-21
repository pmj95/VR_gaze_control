def get_mean_time_data(all_dicts):
    total_times_big = [[], [], [], []]
    total_times_small = [[], [], [], []]
    mean_times_big = []
    mean_times_small = []
    for dicts in all_dicts:
        total_times_big[0].append(dicts[0][0]["stopTime"] - dicts[0][0]["startTime"])
        total_times_big[1].append(dicts[1][0]["stopTime"] - dicts[1][0]["startTime"])
        total_times_big[2].append(dicts[2][0]["stopTime"] - dicts[2][0]["startTime"])
        total_times_big[3].append(dicts[3][0]["stopTime"] - dicts[3][0]["startTime"])
        total_times_small[0].append(dicts[0][1]["stopTime"] - dicts[0][1]["startTime"])
        total_times_small[1].append(dicts[1][1]["stopTime"] - dicts[1][1]["startTime"])
        total_times_small[2].append(dicts[2][1]["stopTime"] - dicts[2][1]["startTime"])
        total_times_small[3].append(dicts[3][1]["stopTime"] - dicts[3][1]["startTime"])

    for control in total_times_big:
        mean_times_big.append(sum(control) / len(control))

    for control in total_times_small:
        mean_times_small.append(sum(control) / len(control))

    return total_times_big, mean_times_big, total_times_small, mean_times_small
