#build the docker image
docker build -t functionapptest .

#run the docker compose file
docker compose up --build