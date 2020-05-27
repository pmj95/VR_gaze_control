import csv
from get_survey_for_prob import get_survey_for_prob


probs = []
with open('data.csv', 'rt', encoding="utf-16") as f:
    data = csv.reader(f, delimiter=';')
    i = 0
    for row in data:
        if i != 0:
            probs.append(get_survey_for_prob(row))
        i += 1

for i in range(4):
    print(probs[i])