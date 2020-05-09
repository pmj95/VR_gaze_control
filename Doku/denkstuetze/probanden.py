import random

probanden = dict()
list_of_combos = [[], [], [], []]


def fill_list():
    for i in range(4):
        for j in range(1, 5):
            for k in range(1, 4):
                for l in range(5):
                    list_of_combos[i].append((j, k))

def get_Value(a, conf, person, i):
    possible_values = get_possible_values(a, conf)

    if len(possible_values) > 0:
        ret_val = possible_values[random.randint(0, len(possible_values) - 1)]
    else:
        for p in probanden:
            trial_set = probanden[p]
            trial = trial_set[i]
            if trial[0] == a:
                break
            if conf == 1:
                if trial[1][0] != a[0] and trial[1][1] == a[1]:
                    poss_values = get_possible_values(trial[0], conf)
                    if len(poss_values) != 0:
                        ret_val = trial[1]
                        trial[1] = poss_values[random.randint(0, len(poss_values) - 1)]
                        list_of_combos[i].append(ret_val)
                        break
            else:
                if trial[1][1] != a[1] and trial[1][0] == a[0]:
                    poss_values = get_possible_values(trial[0], conf)
                    if len(poss_values) != 0:
                        ret_val = trial[2]
                        trial[2] = poss_values[random.randint(0, len(poss_values) - 1)]
                        list_of_combos[i].append(ret_val)
                        break
    return ret_val


def get_possible_values(a, conf):
    possible_values = []
    for combo in list_of_combos[i]:
        if conf == 1:
            if combo[0] != a[0] and combo[1] == a[1]:
                possible_values.append(combo)
        else:
            if combo[1] != a[1] and combo[0] == a[0]:
                possible_values.append(combo)
    return possible_values



def get_measurments(index):
    previous_chosen_count = 0
    values = []
    for i in range(4):
        a = list_of_combos[i][random.randint(0, (20-index)*3)]
        b = (0, 0)
        c = (0, 0)
        while True:
            b = get_Value(a, 1, index, i)
            if b[0] != a[0] and b[1] == a[1]:
                break
        while True:
            c = get_Value(a, 2, index, i)
            if c[1] != a[1] and c[0] == a[0]:
                break
        list_of_combos[i].remove(a)
        list_of_combos[i].remove(b)
        list_of_combos[i].remove(c)
        values.append((a, b, c))
    return values


fill_list()
for i in range(20):
    probanden[i] = get_measurments(i)

print(list_of_combos)


