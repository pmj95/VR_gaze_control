def get_survey_for_prob(row):
    formatted_data = format_data(row)
    return formatted_data


def format_data(row):
    new_data = []
    del row[0]
    del row[0]
    del row[0]
    del row[0]
    del row[0]
    del row[0]
    new_data.append(get_control_method(row, 0))
    new_data.append(get_control_method(row, 15))
    new_data.append(get_control_method(row, 30))
    new_data.append(get_control_method(row, 45))
    for i in range(12):
        del row[-1]
    new_data.append((row[-7], row[-6], row[-5], row[-4]))
    new_data.append(row[-3])
    new_data.append(row[-2])
    new_data.append(row[-1])
    return new_data


def get_control_method(row, offset):
    meth_data = []
    meth_data.append((row[offset], row[offset + 1], row[offset + 2]))
    for i in range(5):
        if row[offset + 4 + i] == '2':
            meth_data.append(i + 1)
    for i in range(5):
        if row[offset + 10 + i] == '2':
            meth_data.append(i + 1)
    return meth_data