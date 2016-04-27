# Senior Project Coding Standard Guidelines
*By Justine Johnson and Josh Ward*

These are a basic set of guidelines and should not be considered complete or holistic.
Their intention is to help set a baseline coding standard for PAML's Senior Project teams.


## The Golden Rule

**Working code is not by default good code.**
Clean; well formatted; easy to read, and therefore easy to maintain and modify; working code is good code.

All other things being equal, when making a choice between the infinite number of ways to write a section of code:
> The simpler, cleaner and easier to read and understand code is the correct choice.


## Code Quality

### Code Formatting and Style
*Every programmer has their strong preferences on everything from positioning of the ubiquitous `{` to spaces vs tabs to `camelCase` vs `snake_case`.
  Most are willing to draw blood over these preferences.*

We are not going to set many standards on code formatting and styling, just two high level rules:
1. **Always prefer the code style of the language you are coding in.**

  Consistency with language conventions such as method and class names and formats or file naming standards
  makes your code take less effort to read and understand.
  Therefore, default language consistency follows *The Golden Rule*.

  *Note: The code examples in this document are C# style and should not be taken as styling guides outside of their intended example*

2. **For everything else the team should have a single "living" standard and religiously enforce it on all of their code.**

  There is a lot of gray left here, therefore the team should reach a team compromise of standards and write them down.
  All members of the team should follow the standards and **importantly have continual conversations updating and revising them.** If the standards aren't working fix them. If they are missing something add to them...
  Use peer reviews and tools to help enforce those standards.
  The primary goal of coding standards is to produce consistent, readable code; Code that adheres to *The Golden Rule*.

  *As a happy secondary, it reduces code formating fights among team members which increases productivity.*


### Naming

Files, Namespaces, Classes, Structures, Fields, Properties, Methods, Parameters, Variables, Type Parameters, and whatever else a developer gets to name in their language of choice should always be given clear meaningful names.

1. **Single letter names are not meaningful.** They force the reader to constantly return to the assignment for context. The reader also needs to maintain the meaning in their head taking their focus away from understanding the logic. Single letter names are often the hardest to understand when used as parameters in function calls.

  Example:
  ```
  Client selectedClient = repository.Clients.Find(s); //What is 's'? I'm now required to read back up the code to establish or regain context.

  Client selectedClient = repository.Clients.Find(selectedClientID); //better gives dramatically more information into the variable
  ```

  Two common "standard" uses of single letter names are in for loops and lambdas as
  parameters names. These, however, still suffer from context issues. Just because
  the context is "smaller" does not mean the reader does not need to maintain the context while reading.

2. **Abbreviations, shortened, or misspelled words are not readable.** Often, they are confusing or at worse even misleading.

  Example:
  ```
  HideMess(); //What mess? So you are going to leave a mess and then just hide it?

  HideMessage(); //Oh Okay.

  HideErrorMessage(); //Even better
  ```

  ```
  IsBDInRng(udob); //Both the method and variable take time to and effort to decipher

  IsBirthDateInRange(userDateOfBirth); //requires no extra effort to read
  ```

  In the age of auto complete and wide screen monitors there is no need to use shortened, hard to decipher abbreviations and words.
  The meaning may seem "completely obvious" to the author while they write the code, however, 
  when the next person comes along, likely the author themselves, they will with out a doubt suffer to understand it.

  ---

  Some abbreviations or shortened words are acceptable because they are common to the english language, programming in general, or the specific domain the code is written for. But these should still be used carefully and documented in the case of domain specific code.

  Examples:
  ```
  ID //Identifier
  GUID //Globally unique identifier
  Int //Integer
  Req //"Requisition" in a specific domain - should be documented in the relevant coding guidelines
  ```

  ---

  Code should be consistently reviewed for spelling errors as they can quickly escalate from mistakes to standards.
  It makes for a frustrating experience if a programmer has to remember that `repository` is actually `reposetory` in certain portions of the code.
  Peer reviews, automated tools, and a healthy amount of care can help minimize accidental misspellings.

3. **Names that only describe type are not very helpful.** Good names should give some context to what the underlying item does or what it is for, not simply what it is.

  Programmers are not creative and often get stuck in a naming routine along the lines of:
  ```
  int id = GetID();
  User user = GetUser(id);
  string name = user.Name;
  ```

  Although not always possible, names can and should give more insight into their context.
  There is a balancing act in verbosity, however, in our experience, the example above tends to be
  the norm and often just a little more effort goes along way.
  ```
  int currentUserID = GetCurrentUserID();
  User currentUser = GetUserFromID(currentUserID);
  string currentUserName = currentUser.Name;
  ```

  In general, always try to give meaningful names. Code reviews and refactoring can be very helpful in accomplishing this often hard chore.


### Comments

Code comments are one of the most powerful and yet most abused and misused tools in coding.

1. **Comments do not replace understandable, "self-documented" code.**
If a comment is needed to explain what code is doing then refactor the code to make it more clear and forgo the comment.

2. **Comments are harder to maintain and require more maintenance than the code itself.**
Code is the actual focus of, *well*, the code, and it has an exact definition controlled by the compiler.
Comments, with some exception, are free-for-all and are often ignored during code changes eventually leading to out of date, orphaned, and misleading comments.

3. **Despite best intentions, comments are often never read by the code readers.**
  Programmers do not read comments because our brains are trained to skip them.
  They are not code so they do not matter.
  This is especially true the more comments there are and the more verbose they are.

4. **Commented code is "evil".**
  There are many reasons why a programmer may wish to temporarily comment out some code.
  However when that code stays commented out past that small relevant scenario, it becomes one of the most
  "evil" comments a programmer can make.

  * Readers and maintainers of the code have no idea why a bunch of it is commented out.
    * Is it important?
    * Was it forgotten about?
    * My team mate must still need this...
    * Can I delete it?
    * Is it there to tell me something?
  * When the code pertaining to it is refactored, the code in the comment becomes uncompilable,
    making commented code degrade even faster than normal comments.
  * Despite all that, commented code unnaturally seems more important than regular comments
    and tends to stick around unchanged even longer.

  Proper use of source control is the solution to commented code. A developer can delete code without fear and
  bring it back without challenge using correct source control practices. As a general rule, do not commit commented out code to source control. Proper discipline to review code and cleanup all commented code once functioning is required. 

5. **Actual code is the best document for "what" it does. Comments can explain "why" and "how".**
  Good comments are sparse, concise and explain things about the code that the code itself cannot say or say completely.
  * "Why is something done this way as opposed to another?"
  * "How does this solve the problem in the correct way?"
  * "How do I use this API in my own code?"
  * "How does this fit in the bigger picture?"


### Strong Typing

In most cases data should be moved around as strongly typed objects. Often data is passed around as rudimentary types:
strings for emails, ints for IDs etc, int x and int y for a point. The problem is that this requires
either total trust on the validity of the data by the consumers or constant rechecking for validity.

In as many cases as possible convert raw data into strongly typed objects that throw exceptions when invalid
and pass around those strongly typed objects.


## Unit Testing

Unit testing is fundamental to effective software engineering.

1. **The goal of unit testing is to enforce desired functionality from isolated sections of code.**
Unit tests describe and enforce functionality in the form of "given this input this is the expected result" or "when I do this, this happens".
Once this behavior is enforced with unit tests it allows the code to be refactored without fear of unknowingly breaking required functionality.

2. **Unit tests should always actively assert that an expected result happened.**
Checking something along the lines of "no exception was thrown
when calling this code" is not an active test and therefore not an effective unit test.
Instead, test the conditions where an exception is the expected result in addition to adding tests checking the expected results for the non exception cases.

3. **A unit test should test one and only one thing.**
This makes simple and easy to maintain tests that are separated from other discrete functionality requirements. The name of the test should clearly describe the functionality under question.

4. **A single unit test should always be organized as "Arrange", "Act", "Assert"**
Always organize into discrete ordered steps:
  1. **Arrange.**
  Set up the mocks, fakes, test values, configuration etc. for the target being tested.
  Also set up the target of the the test (as long as that setup is not part of what is being tested.)
  There is a lot of potential duplication in this step among similar tests; Move that duplication into helper methods.

  2. **Act.**
  Preform the actual behavior being tested. There should be no asserting here.
  Instead, capture returns in variables to be asserted on later. Expected exceptions should also be captured here to be asserted later.

  3. **Assert.**
  Validate that the values captured in the Act step meet the expectations.
  Validate mocks here if required (i.e. some method on the mock was called 3 times with particular parameters).

5. **Unit tests should test a section of code in isolation.**
Most of the time when testing class A which requires services from class B, class B should be an interfaced composite dependency (loosely coupled). This allows class A to be tested without requiring class B to be working as expected. Class B will be tested separately.
Note that supporting this style of unit testing will most likely require the application to use dependency injection.

6. **100% code coverage is unrealistic and often undesirable.**
Unit tests should never call or work with external components such as files, databases, web services etc.
All of those components should be interfaced away then mocked or faked to provide the consistent behavior the unit tests are expecting.
This means those actual components cannot be unit tested and trying to do so will often create a fragile unit test. However, the "un-unit-testable" part of any project's code should be a small percent.

7. **Unit tests are code too.** This means all other coding best practices should be applied when writing unit tests.
Create helper methods, use good naming conventions, do not repeat yourself (which is easy to do in unit testing).
If unit tests are hard to read and maintain they will not be maintained.

8. **Broken tests are broken code.**
Failing unit tests should be treated with the same severity as code that fails to compile.
Code added to primary source control branches should compile as well as pass all the unit tests.

9. **Write unit tests first.**
This is the main philosophy of test-driven-design.
Often if tests are written after the code the required functionality of the component in question has not been well thought out.
Furthermore, when the tests are finally written (if at all) the tests are written
around the exact functionality that was built and not necessarily the functionality
that was required.
Writing the tests first requires thought and definition about the functionality of the component as
well as thought about how to make that functionality testable.

10. **Unit tests do not replace traditional software testing.**
Unit tests only prove that small isolated pieces of code do what is expected.
It is the job of other testing methods (automated and manual) to prove that those pieces work together as expected.