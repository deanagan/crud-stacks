import uuid
import random
import string

cmd_fmt = 'INSERT INTO [dbo].[Todo]([Id], [UniqueId], [Summary], [Detail], [IsDone], [AssigneeGuid])'

summary_gen = lambda: ''.join(random.choices(string.ascii_letters + string.digits, k=8))
detail_gen = lambda: ''.join(random.choices(string.ascii_letters + string.digits + ' ', k=50))
is_done_gen = lambda: random.choice(range(2))


def get_random_entries(index):
    return (index, uuid.uuid4(), summary_gen(), detail_gen(), is_done_gen())


print(cmd_fmt)
for i in range(1, 1001):
    id, unique_id, summary, detail, is_done = get_random_entries(i)
    value_fmt = f"({id}, '{unique_id}', '{summary}', '{detail}', {is_done}, null),"
    print(value_fmt)
