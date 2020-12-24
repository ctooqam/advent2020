input = "739862541"


values = {int(input[i]): int(input[i+1]) for i in range(len(input)-1)}
values[int(input[len(input)-1])] = 10
for i in range(10, 1000001):
    values[i] = i+1
values[1000000] = int(input[0])

current = int(input[0])

min_cup = min(values)
max_cup = max(values)

for i in range(10000000):
    n1 = values[current]
    n2 = values[n1]
    n3 = values[n2]
    destination = current-1 if min_cup != current else max_cup
    while(destination in [n1, n2, n3]):
        destination = destination-1 if min_cup != destination else max_cup
    values[current] = values[n3]
    values[n3] = values[destination]
    values[destination] = n1
    current = values[current]

value = 1
n1 = values[value]
n2 = values[n1]
res = n1*n2
print(res)
