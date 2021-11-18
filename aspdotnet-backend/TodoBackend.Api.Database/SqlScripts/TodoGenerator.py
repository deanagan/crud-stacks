import uuid
import random
import string

summary_gen = lambda: ''.join(random.choices(string.ascii_letters + string.digits, k=8))
detail_gen = lambda: ''.join(random.choices(string.ascii_letters + string.digits + ' ', k=50))
is_done_gen = lambda: random.choice(range(2))


def get_random_entries(index):
    return (index, uuid.uuid4(), summary_gen(), detail_gen(), is_done_gen())

total_todos = 20
for i in range(1, total_todos):
    id, unique_id, summary, detail, is_done = get_random_entries(i)
    value_fmt = f"({id}, '{unique_id}', '{summary}', '{detail}', {is_done}, null){',' if i < total_todos -1 else ''}"
    print(value_fmt)
