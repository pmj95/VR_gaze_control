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

print(probs[3])
print(probs[0])
print(probs[1])
print(probs[2])
