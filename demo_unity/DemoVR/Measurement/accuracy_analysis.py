import re
import pandas as pd


def get_accuracy(all_dicts):
    accuracies_big = [[], [], [], []]
    accuracies_small = [[], [], [], []]
    for dicts in all_dicts:
        accuracies_big[0].append(get_single_acc(dicts[0][0]["action"]))
        accuracies_big[1].append(get_single_acc(dicts[1][0]["action"]))
        accuracies_big[2].append(get_single_acc(dicts[2][0]["action"]))
        accuracies_big[3].append(get_single_acc(dicts[3][0]["action"]))
        accuracies_small[0].append(get_single_acc(dicts[0][1]["action"]))
        accuracies_small[1].append(get_single_acc(dicts[1][1]["action"]))
        accuracies_small[2].append(get_single_acc(dicts[2][1]["action"]))
        accuracies_small[3].append(get_single_acc(dicts[3][1]["action"]))

    mean_accuracies_big = []
    mean_accuracies_small = []
    for accuracy in accuracies_big:
        mean_accuracies_big.append(sum(accuracy) / len(accuracy))

    for accuracy in accuracies_small:
        mean_accuracies_small.append(sum(accuracy) / len(accuracy))

    return accuracies_big, mean_accuracies_big, accuracies_small, mean_accuracies_small


def get_single_acc(action):
    true_count = 0
    false_count = 0
    reg = re.compile(r'True')
    for act in action:
        if reg.search(act):
            true_count += 1
        else:
            false_count += 1
    acc = true_count/(true_count + false_count)
    return acc


def create_bar_graph_accuracy(data):
    new_data_big = [[], [], [], [], []]
    new_data_small = [[], [], [], [], []]
    for i in range(4):
        new_data_big[0].append(data[0][i][0])
        new_data_big[1].append(data[0][i][1])
        new_data_big[2].append(data[0][i][2])
        new_data_big[3].append(data[0][i][3])
        new_data_big[4].append(data[1][i])
        new_data_small[0].append(data[2][i][0])
        new_data_small[1].append(data[2][i][1])
        new_data_small[2].append(data[2][i][2])
        new_data_small[3].append(data[2][i][3])
        new_data_small[4].append(data[3][i])

    data_labels = ['Laser/Trigger', 'Laser/Blink', 'Eye/Trigger', 'Eye/Blink']
    df_big = pd.DataFrame(
        {'Prob1': new_data_big[0], 'Prob2': new_data_big[1], 'Prob3': new_data_big[2], 'Prob4': new_data_big[3],
         'Mean': new_data_big[4]}, index=data_labels)
    plot_big = df_big.plot.barh(fontsize=7.5)
    plot_big.set_xlabel('Accuracy')
    plot_big.set_ylabel('Control Method')
    plot_big.figure.savefig("plot_acc_big.png", format='png', dpi=200)

    df_small = pd.DataFrame(
        {'Prob1': new_data_small[0],
         'Prob2': new_data_small[1],
         'Prob3': new_data_small[2],
         'Prob4': new_data_small[3],
         'Mean': new_data_small[4]}, index=data_labels)
    plot_small = df_small.plot.barh(fontsize=7.5)
    plot_small.set_xlabel('Accuracy')
    plot_small.set_ylabel('Control Method')
    plot_small.figure.savefig("plot_acc_small.png", format='png', dpi=200)
