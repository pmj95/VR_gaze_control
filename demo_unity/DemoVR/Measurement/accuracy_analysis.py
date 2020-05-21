import re


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


