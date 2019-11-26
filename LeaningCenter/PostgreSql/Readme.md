# Docker 安装 PgSql

    docker pull postgres

    docker run --name postgres1 -e POSTGRES_PASSWORD=Cloud@1234 -e POSTGRES_USER=postgres -v /Users/luweiping/home/data:/var/lib/postgresql/data/pgdata -d -p 5432:5432 postgres
    # 或者
    docker run --name postgres1 -e POSTGRES_PASSWORD=Cloud@1234 -e POSTGRES_USER=postgres - -d -p 5432:5432 postgres


## Postgresql 教程

* [postgresql 教程参考](https://www.yiibai.com/postgresql/postgresql-create-table.html)