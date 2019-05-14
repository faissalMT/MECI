DOCKER_COMPOSE = docker-compose -f docker-compose.yml


.PHONY: setup
setup: docker-build

.PHONY: docker-build
docker-build:
	$(DOCKER_COMPOSE) build

.PHONY: docker-down
docker-down:
	$(DOCKER_COMPOSE) down

.PHONY: serve
serve: docker-down docker-build
	$(DOCKER_COMPOSE) up

.PHONY: shell
shell:
	$(DOCKER_COMPOSE) run --rm web bash
