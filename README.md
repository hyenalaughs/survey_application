Чтобы запустить:
1. Скачайте ZIP архив и разархивируйте.
2. Запустите Docker-Desktop.
3. Запустите терминал в директории скачанного проекта.
4. Введите команду: docker-compose up --build

api: http://localhost:5000/Swagger/Index.html
db: http://localhost:5438

Чтобы проверить работоспособность БД в терминале: psql -h localhost -p 5438 -U surveyuser -d surveydb
Когда попросит пароль, введите: surveypass

Если используете DBeaver или pgAdmin то:
host: localhost или 127.0.0.1
port: 5438
user: surveyuser
pass: surveypass
db-name: surveydb
name: surveydb

