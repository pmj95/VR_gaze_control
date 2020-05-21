import glob, json


def sort_dicts(dicts):
    final_dicts = [[0, 0], [0, 0], [0, 0], [0, 0]]
    x = 0
    y = 0
    for dict in dicts:
        if dict["currentState"] == "LaserTrigger":
            x = 0
        elif dict["currentState"] == "LaserBlinking":
            x = 1
        elif dict["currentState"] == "EyeTrigger":
            x = 2
        elif dict["currentState"] == "BlinkingEye":
            x = 3

        if dict["scaling"] == "Large":
            y = 0
        else:
            y = 1

        final_dicts[x][y] = dict
    return final_dicts


def get_dicts(folder_path):
    temp_dicts = []
    for file in glob.glob(folder_path + "/*.json"):
        with open(file) as f:
            temp_dicts.append(json.load(f))
    return sort_dicts(temp_dicts)
