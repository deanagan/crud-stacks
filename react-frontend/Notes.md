# React Frontend


## Project Setup

This project was not originally typescript but was converted into typescript via the following steps:

1. npm install --save typescript @types/node @types/react @types/react-dom @types/jest
2. Renamed all .js/x to .ts/x files.
3. Used React.FC.


## Adding Redux

1. Adding needed packages:

    ```
    npm install redux react-redux redux-thunk
    ```

2. Adding types (newer versions don't need types for react-redux and redux-thunk):

    ```
    npm install @types/redux
    ```

## Running to connect to backend

At some point in development, I was disabling cert errors when I launch the browser. But then I stumbled upon Scott Hanselman's old post:
https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs

This made me run the command: dotnet dev-certs https --trust

This post was is old and was on dotnet 2.1. I've since moved on to 3.1 and later,
and I think useHsts may be enough.