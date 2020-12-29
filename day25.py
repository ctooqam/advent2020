

remainder = 20201227
subject_number = 7

f = open('data/data25.txt')

public_keys = []
for line in f:
    public_keys.append(int(line.strip()))
print(public_keys)


value = 1
loop_sizes = []
for key in public_keys:
    value = 1
    loop_size = 0
    while(value != key):
        loop_size += 1
        value = value * subject_number % remainder
    loop_sizes.append(loop_size)

value = 1
for i in range(loop_sizes[0]):
    value = value * public_keys[1] % remainder
print(value)
value = 1
for i in range(loop_sizes[1]):
    value = value * public_keys[0] % remainder
print(value)
