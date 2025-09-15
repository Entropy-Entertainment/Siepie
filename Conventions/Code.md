# Code Conventions

## Naming Conventions
All non standard data objects should always be in PascalCase.<br>
Think of enums, classes, structs, interfaces, and type aliases.

For variables, functions, and methods use camelCase if they are private.<br>
Outside of that, use PascalCase.

## File Naming
File names should always be in PascalCase and their name shouldn't be abbreviated and longer than 3 words.They should also reflect their [SRP](https://www.geeksforgeeks.org/system-design/single-responsibility-in-solid-design-principle/) purpose.<br>

## File Structure
Files should always start with imports, then constants, then variables, then functions.<br>
These should also be sorted from public, internal, to private (so public functions should be the first functions see when you look through the file). <br> <b>Writing on the same line of code isn't allowed after a semicolon.</b>

