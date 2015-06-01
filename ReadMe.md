# Testing and Refactoring Legacy Code

.NET version of Sandro Mancuso refactoring kata (see links).

## Code

    git clone https://github.com/orient-man/TripServiceKata.git
    cd TripServiceKata
    git checkout demo-start

### Navigating refactoring steps with git

Useful git aliases:

	prev = checkout HEAD^1
    next = "!git checkout `git rev-list HEAD..demo-end | tail -1`"
    goto = "!f() { git reset --hard && git clean -fd && git checkout $1; }; f"

## Context

Imagine a social networking website for travelers

 * You need to be logged in to see the content
 * You need to be a friend to see someone else's trips

## Legacy Code Rules

 * You cannot change production code if not covered by tests
  - Just automated ("safe") refactorings (via IDE) are allowed, if needed to write the test

## Testing tips

Start testing from shortest do deepest branch

 * because getting to deepest requires big setup i.e. sample data, mocks, fakes etc.)
 * allows to understand better what the code does

## Refactoring tips

Starting from the deepest branch to the shortest (different than testing)
 
 * method does too much (feature envy)
 * Single Responsibility Principle!

## Craftsmen at work

 * Write readable and maintainable code
  - code must express business rules
 * Strive for simplicity
 * Know your tools well (i.e. frameworks, editor)
 * Work in small and safe increments
  - commit often
 * Embrace change, be brave
 * Boy scout rule / No broken windows

# Links

 * [Working Effectively with Legacy Code](http://www.amazon.com/Working-Effectively-Legacy-Michael-Feathers/dp/0131177052)
 * [Refactoring: Improving the Design of Existing Code](http://www.amazon.com/Refactoring-Improving-Design-Existing-Code/dp/0201485672/)
 * [Clean Code: A Handbook of Agile Software Craftsmanship](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
 * [The Software Craftsman: Professionalism, Pragmatism, Pride](http://www.amazon.com/The-Software-Craftsman-Professionalism-Pragmatism/dp/0134052501)
 * [Original talk video (Java)](http://www.youtube.com/watch?v=_NnElPO5BU0)
