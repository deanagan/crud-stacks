# Wait for SQL Server to be started
./wait-for-it.sh database:14331 --timeout=0 --strict -- sleep 5s