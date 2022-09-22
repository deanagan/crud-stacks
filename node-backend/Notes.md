# Backend Code Notes

## The backend was created using the following steps to begin with:

1. Setup package.json file:
    `npm init -y (or no '-y' if you need to answer entries manually)`

2. Add typescript as a dev dependency
    `npm install typescript --save-dev`

3. Install ambient node.js types for typescript:
    `npm install @types/node --save-dev`

4. Create tsconfig.json file. Clean up generated file as needed.

    `npx tsc --init --rootDir src --outDir build --esModuleInterop --resolveJsonModule --lib es6 --module commonjs --allowJs true --noImplicitAny true`

5. Add entry point file. Add folder src and in it, index.ts ( add code, at least console.log and run with `npx tsc`)

6. Add code reloading:
    `npm install --save-dev ts-node nodemon`

7. Add a nodemon.json config
    `{
        "watch": ["src"],
        "ext": ".ts,.js",
        "ignore": [],
        "exec": "ts-node ./src/index.ts"
    }`

8. Add a script command in package.json
    `"develop": "nodemon",`

9. Install rimraf for cleaning up builds
    `npm install --save-dev rimraf`

10. Add a build command in package json
    `"build": "rimraf ./build && tsc",`

11. Add a production start up script
    `"start": "npm run build && node build/index.js"`

12. Setup eslint
    `npm install --save-dev eslint @typescript-eslint/parser @typescript-eslint/eslint-plugin`

13. Create .eslintrc file with the following:
    `{
        "root": true,
        "parser": "@typescript-eslint/parser",
        "plugins": [
            "@typescript-eslint"
        ],
        "extends": [
            "eslint:recommended",
            "plugin:@typescript-eslint/eslint-recommended",
            "plugin:@typescript-eslint/recommended"
        ]
    }`

14. Create .eslintignore to ignore build, node_modules

15. Add a lint script to package.json.
    `"lint": "eslint . --ext .ts",`

16. Add a no-console rule to our linter in production mode.
    `"rules": {
        "@typescript-eslint/indent": ["error", 2],
        "@typescript-eslint/no-unused-vars": "error",
        "@typescript-eslint/no-explicit-any": "error"
    }`

17. Add prettier for formatting.
    `npm install --save-dev prettier`

18. Create .prettierrc and add:
    `{
        "semi": true,
        "trailingComma": "es5",
        "singleQuote": false,
        "printWidth": 120
    }`

19. Add eslint-plugin-prettier:
    `npm install --save-dev eslint-plugin-prettier`

20. Install these packages to make prettier and eslint work together:
    `npm install --save-dev eslint-config-prettier eslint-plugin-prettier`

20. Add prettier to plugins and extends, then add to rules:
    `
   "plugins": [
      ...
      "prettier"
    ],
    "extends": [
      ...
      "plugin:prettier/recommended"
    ],
    "rules": {
        ...
        "prettier/prettier": "warn,
    }
    `


21. Add express to the project:
    `npm install express @types/express`

To test, add the following to index.ts:
```typescript
import express from 'express';

const app = express();
const PORT = 8081;
app.get('/hello', (req, res) => res.send('Hello basic setup!'));
app.listen(PORT, () => {
  console.log(`[server]: Server is running at https://localhost:${PORT}`);
});
```

Then using postman, send:
`http://localhost:8081/hello`

should see: `Hello basic setup!`


22. To run, `npm run develop`