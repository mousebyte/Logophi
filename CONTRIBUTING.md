# Contributing

Wanna help make Logophi better? That's great! Here are some general guidelines.

## Issues
When submitting an issue, always use the provided issue templates. If they don't cover what you need, contact me directly and I will
try to help (or make a new issue template).

## Code Contributions

* Keep formatting and style consistent!
  * Braces for type and namespace should be K&R style (opening brace at the end of the line, after a space). Property and accessor
  definitions should be K&R style as well, except auto properties, which should be on a single line. Everything else should be
  Whitesmiths style (opening brace on next line, both braces and code indented once).
  * Single line accessors may be expression bodies, but member functions may not.
  * Class fields should be `_lowerCamelCase` starting with an underscore. Everything else should be `UpperCamelCase`.
  * All fields that can be readonly, should be readonly. Always use properties instead of public fields.
  * When modifying the UI, be sure to use the [MVP pattern](https://en.wikipedia.org/wiki/Model–view–presenter). There should be no
    conditional logic within views (forms). Use the included MVP classes.
  * Everything should have XML documentation comments before making a pull request. In-line comments are appreciated, especially for
    tricky logic, but they not strictly necessary.
* Be sure to test your contribution. This should go without saying, but it should compile and function as intended. I will help with
  testing when you make your pull request.
  
  Code style examples:
    ```C#
    namespace MouseNet {
    
        public class Foo {
            private bool _aBool;
            
            int AnInt { get; set; }
            
            bool AnotherBool {
                get => _aBool;
                set {
                if (value) DoStuff();
                }
            }
            
            void Bar()
                {
                ...
                }
        }
    }
    ```
