import uuid
import random
import string



string_gen = lambda: ''.join(random.choices(string.ascii_letters + string.digits, k=8))
email_gen = lambda: f"{''.join(random.choices(string.ascii_letters + string.digits, k=8))}@todo.io"
role_ids = lambda: random.choice(['B7D50830-622B-443C-8BC8-AAB6D1C6C3C4', '804F7003-5777-4471-B1D4-B793D3FB643C'])
bool_gen = lambda: random.choice(range(2))


def get_random_entries(index):
    first_name, last_name = string_gen(), string_gen()
    username = f"{first_name}.{last_name}"
    normalised_username = username.upper()
    email = email_gen()
    normalised_email = email.upper()
    email_confirmed = bool_gen()
    password_hash = string_gen()
    phone = string_gen()
    phone_confirmed = bool_gen()
    two_factor_enabled = bool_gen()
    is_deleted = 0
    role_unique_id = role_ids()

    return (index, uuid.uuid4(), username, normalised_username, first_name, last_name, email, normalised_email,\
        email_confirmed, password_hash, phone, phone_confirmed, two_factor_enabled, \
        is_deleted, role_unique_id)

num_users = 50

for i in range(1, num_users):
    id, unique_id, username, normalised_username, first_name, last_name, email, normalised_email,\
        email_confirmed, password_hash, phone, phone_confirmed, two_factor_enabled, \
        is_deleted, role_unique_id = get_random_entries(i)
    value_fmt = f"({id}, '{unique_id}', '{username}', '{normalised_username}', '{email}', \
'{first_name}', '{last_name}',\
'{normalised_email}', {email_confirmed}, '{password_hash}', '{phone}', {phone_confirmed},\
{two_factor_enabled}, {is_deleted}, '{role_unique_id}')"
    print(value_fmt, ',' if i < (num_users - 1) else '')
