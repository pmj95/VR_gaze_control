import random

probanden = dict()
number_of_tests = 5
number_of_probs = 20
list_of_combos = []
x_list = [[], [], [], []]
y_list = [[], [], []]
x_needed = [0, 0, 0, 0]
y_needed = [0, 0, 0]


def fill_list():
    for j in range(1, 4):
        for k in range(1, 5):
            for l in range(number_of_tests):
                list_of_combos.append((j, k))


def get_a():
    ret_val = list_of_combos[random.randint(0, len(list_of_combos) - 1)]
    list_of_combos.remove(ret_val)
    return [ret_val, 0, 0]


def get_value_from_x(prob_x):
    b = (0, 0)
    c = random.randint(0, 0)
    while True:
        b = x_list[prob_x[0][1] - 1][random.randint(0, len(x_list[prob_x[0][1] - 1]) - 1)]
        if b != prob_x[0]:
            if y_needed[b[0] - 1] <= len(y_list[b[0] - 1]) - 1:
                break
    x_list[b[1] - 1].remove(b)
    y_list[b[0] - 1].remove(b)
    prob_x[1] = b
    return b

def get_value_from_y(prob_y):
    c = (0, 0)
    while True:
        upper = len(y_list[prob_y[0][0] - 1]) - 1
        c = y_list[prob_y[0][0] - 1][random.randint(0, upper)]
        if c != prob_y[0]:
            break
    x_list[c[1] - 1].remove(c)
    y_list[c[0] - 1].remove(c)
    prob_y[2] = c
    return b

fill_list()
for i in range(number_of_probs):
    probanden[i] = get_a()
for item in list_of_combos:
    if item[1] == 1:
        x_list[0].append(item)
    elif item[1] == 2:
        x_list[1].append(item)
    elif item[1] == 3:
        x_list[2].append(item)
    elif item[1] == 4:
        x_list[3].append(item)

for prob in probanden:
    if probanden[prob][0][1] == 1:
        x_needed[0] = x_needed[0] + 1
    elif probanden[prob][0][1] == 2:
        x_needed[1] = x_needed[1] + 1
    elif probanden[prob][0][1] == 3:
        x_needed[2] = x_needed[2] + 1
    elif probanden[prob][0][1] == 4:
        x_needed[3] = x_needed[3] + 1

for prob in probanden:
    if probanden[prob][0][0] == 1:
        y_needed[0] = y_needed[0] + 1
    elif probanden[prob][0][0] == 2:
        y_needed[1] = y_needed[1] + 1
    elif probanden[prob][0][0] == 3:
        y_needed[2] = y_needed[2] + 1

for item in list_of_combos:
    if item[0] == 1:
        y_list[0].append(item)
    elif item[0] == 2:
        y_list[1].append(item)
    elif item[0] == 3:
        y_list[2].append(item)

for i in range(3):
    if x_needed[i] > len(x_list[i]):
        raise ValueError("Stupid idiot")
    if y_needed[i] > len(y_list[i]):
        raise ValueError("Stupid idiot")

for prob in probanden:
    b = get_value_from_x(probanden[prob])
for prob in probanden:
    c = get_value_from_y(probanden[prob])
print(probanden)
print("fuck off")



"""

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
    for i in range(2):
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
for i in range(5):
    probanden[i] = get_measurments(i)

print(list_of_combos)
"""

