from collections import Counter

f = open('data/data24.txt')

delta_dir = {
    'w': (-1, 0),
    'e': (1, 0),
    'sw': (-1, -1),
    'se': (0, -1),
    'nw': (0, 1),
    'ne': (1, 1)
}

all_pos = []

for line in f:
    i = 0
    pos = (0, 0)
    while(i < len(line.strip())):
        if(i != len(line.strip()) - 1 and line[i:i+2] in delta_dir):
            d = delta_dir[line[i:i+2]]
            pos = (pos[0] + d[0], pos[1] + d[1])
            i += 2
        else:
            d = delta_dir[line[i]]
            pos = (pos[0] + d[0], pos[1] + d[1])
            i += 1
    all_pos.append(pos)

count = Counter(all_pos)
black_tiles = list(filter(lambda x: count[x] % 2 == 1, count))
print(len(black_tiles))


def count_neighbours(pos, black_tiles):
    nbr_of_black = 0
    for neighbour in delta_dir.values():
        if ((pos[0] + neighbour[0], pos[1] + neighbour[1]) in black_tiles):
            nbr_of_black += 1
    return nbr_of_black


days = 100
black_tiles = set(black_tiles)
for i in range(days):
    visited = set()
    next_day = set()
    for tile in black_tiles:
        nbr_of_neighbous = count_neighbours(tile, black_tiles)
        if(nbr_of_neighbous in [1, 2]):
            next_day.add(tile)
        for neighbour in delta_dir.values():
            neighbour_tile = (tile[0] + neighbour[0], tile[1] + neighbour[1])
            if(neighbour_tile in visited):
                continue
            nbr_of_neighbous = count_neighbours(neighbour_tile, black_tiles)
            if neighbour_tile not in black_tiles and nbr_of_neighbous == 2:
                next_day.add(neighbour_tile)
            elif neighbour_tile in black_tiles and nbr_of_neighbous in [1, 2]:
                next_day.add(neighbour_tile)
            visited.add(neighbour_tile)
        visited.add(tile)
    black_tiles = next_day
    print(len(black_tiles), i+1)
