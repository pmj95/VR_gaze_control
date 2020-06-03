import pandas as pd

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


def create_bar_graph(data):
    new_data_big = [[], [], [], [], []]
    new_data_small = [[], [], [], [], []]
    font = {'family': 'DejaVu Sans',
            'color': 'black',
            'weight': 'normal',
            'size': 7,
            }
    for i in range(4):
        new_data_big[0].append(data[0][i][0]/1000)
        new_data_big[1].append(data[0][i][1]/1000)
        new_data_big[2].append(data[0][i][2]/1000)
        new_data_big[3].append(data[0][i][3]/1000)
        new_data_big[4].append(data[1][i]/1000)
        new_data_small[0].append(data[2][i][0] / 1000)
        new_data_small[1].append(data[2][i][1] / 1000)
        new_data_small[2].append(data[2][i][2] / 1000)
        new_data_small[3].append(data[2][i][3] / 1000)
        new_data_small[4].append(data[3][i] / 1000)

    data_labels = ['Laser/Trigger', 'Laser/Blink', 'Eye/Trigger', 'Eye/Blink']
    df_big = pd.DataFrame({'Prob1': new_data_big[0], 'Prob2': new_data_big[1], 'Prob3': new_data_big[2], 'Prob4': new_data_big[3],
                            'Mean': new_data_big[4]}, index=data_labels)
    plot_big = df_big.plot.barh(fontsize=7.5)
    offset_low = - 0.245
    offset_high = 0.165
    d = (offset_high - offset_low) / 4
    for index, value in enumerate(new_data_big[0]):
        plot_big.text(value, index + offset_low, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_big[1]):
        plot_big.text(value, index + offset_low + d * 1, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_big[2]):
        plot_big.text(value, index + offset_low + d * 2, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_big[3]):
        plot_big.text(value, index + offset_low + d * 3, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_big[4]):
        plot_big.text(value, index + offset_high, str(round(value, 3)), fontdict=font)
    plot_big.set_xlabel('Time')
    plot_big.set_ylabel('Control Method')
    plot_big.set_title('Total Time Needed to Complete Test Run with Big Buttons')
    plot_big.figure.savefig("plot_big.png", format='png', dpi=200)

    df_small = pd.DataFrame(
        {'Prob1': new_data_small[0],
         'Prob2': new_data_small[1],
         'Prob3': new_data_small[2],
         'Prob4': new_data_small[3],
         'Mean': new_data_small[4]}, index=data_labels)
    plot_small = df_small.plot.barh(fontsize=7.5)
    offset_low = - 0.245
    offset_high = 0.165
    d = (offset_high - offset_low) / 4
    for index, value in enumerate(new_data_small[0]):
        plot_small.text(value, index + offset_low, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_small[1]):
        plot_small.text(value, index + offset_low + d * 1, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_small[2]):
        plot_small.text(value, index + offset_low + d * 2, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_small[3]):
        plot_small.text(value, index + offset_low + d * 3, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_small[4]):
        plot_small.text(value, index + offset_high, str(round(value, 3)), fontdict=font)
    plot_small.set_xlabel('Time')
    plot_small.set_ylabel('Control Method')
    plot_small.set_title('Total Time Needed to Complete Test Run with Small Buttons')
    plot_small.figure.savefig("plot_small.png", format='png', dpi=200)


def create_bar_graph_increase_time(data):
    new_data_increase = [[], [], [], [], []]
    font = {'family': 'DejaVu Sans',
            'color': 'black',
            'weight': 'normal',
            'size': 7,
            }
    for i in range(4):
        new_data_increase[0].append(data[2][i][0] / data[0][i][0])
        new_data_increase[1].append(data[2][i][1] / data[0][i][0])
        new_data_increase[2].append(data[2][i][2] / data[0][i][0])
        new_data_increase[3].append(data[2][i][3] / data[0][i][0])
        new_data_increase[4].append(data[3][i] / data[1][i])

    print(new_data_increase)
    data_labels = ['Laser/Trigger', 'Laser/Blink', 'Eye/Trigger', 'Eye/Blink']
    df_big = pd.DataFrame(
        {'Prob1': new_data_increase[0],
         'Prob2': new_data_increase[1],
         'Prob3': new_data_increase[2],
         'Prob4': new_data_increase[3],
         'Mean': new_data_increase[4]}, index=data_labels)
    plot_big = df_big.plot.barh(fontsize=7.5)
    offset_low = - 0.245
    offset_high = 0.165
    d = (offset_high - offset_low) / 4
    for index, value in enumerate(new_data_increase[0]):
        plot_big.text(value, index + offset_low, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_increase[1]):
        plot_big.text(value, index + offset_low + d * 1, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_increase[2]):
        plot_big.text(value, index + offset_low + d * 2, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_increase[3]):
        plot_big.text(value, index + offset_low + d * 3, str(round(value, 3)), fontdict=font)
    for index, value in enumerate(new_data_increase[4]):
        plot_big.text(value, index + offset_high, str(round(value, 3)), fontdict=font)
    plot_big.set_title('Relative Change in Time with Buttons with Half the Diameter')
    plot_big.set_xlabel('Time Increase')
    plot_big.set_ylabel('Control Method')
    plot_big.figure.savefig("plot_time_increase.png", format='png', dpi=200)

