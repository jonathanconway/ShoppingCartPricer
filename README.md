# Shopping Cart Pricer Exercise - Notes

Here are some notes that the reviewer may find helpful or clarifying.

## Production quality

The guidelines mentioned that the solution should be of 'production quality'.

I worked off the characteristics of quality espoused in the books Code Complete (S. McConnell) and Object-Oriented Software Constructions (B. Meyer).

Based on the requirements, constraints and scope of this exercise, I considered the following aspects of production quality:

* Correctness - Code functions according to the requirements given.
* Accuracy - Calculations are free from quantitative errors. Appropriately precise type (decimal) is used.
* Functionality - The code produces the required calculation functionality.
* Extensibility - Simple, decoupled design. Rules can be added/removed independently.
* Testability, Verifiability - Rules, Items, BasketEntries and PriceCalculator are decoupled and can be easily isolated for testing and reasoning about. Unit tests are included.
* Scalable, Efficient - Time and space efficient in relation to inputs.
* Portability, Adaptability, Compatibility - Implemented in .NET Core; can be used across platforms. The module can be integrated into a broader solution, e.g. a Shopping Cart website or a mobile app.
* Readability and Maintainability - Class, field and methods are named carefully. Familiar design patterns are used - Factory, Strategy. Simple algorithms are used to ease readability.
* Robustness - Handles invalid inputs and boundary conditions / edge cases by means of assertions (e.g. total shouldn't fall below zero after discounts are applied). Uses constraints (such as read-only properties) to limit the range of run-time behaviour, preventing unexpected operations from being performed.

I deliberately didn't consider the following aspects of production quality:

* Security. Doesn't seem applicable, given the scope of this exercise. Would need to be considered in designing and deploying infrastructure, API Gateway, web or mobile application code, etc.


## Commentary on implementation

* The Item and Entry classes model items (e.g. toothbrush, potato) and entries of those items into a shopping cart (e.g. 1 toothbrush, 3 potatoes).

* The IDiscountRule interface provides a consistent interface across any rules that need to be created. Thus, different rules can be treated abstractly and composed together in code and/or at run-time.

* The DiscountRuleFactory generates one instance of each IDiscountRule.

* PriceCalculator calculates the total price including any discounts. DiscountRules are injected into the constructor. This allows a given instance of PriceCalculator to be re-used across multiple calculations. In theory, this would reduce overhead when dealing with a large number of requests, but this should be verified with load testing. I consider load testing to be beyond the scope of this exercise.

* In theory (though not present in this code) higher-order or composite rules could be created. These are rules which call other rules, generating new behaviour. For example: a BuyTwoGetOneFreeInWinter rule could apply the BuyTwoGetOneFree rule only during winter months. 


# Assumptions

I made a few simplifying assumptions, based on the guideline of two hours, the scope of requirements given and a bias toward simplicity and against over-engineering.

* Each Discount rule is individually responsible for ensuring that its discount is applied correctly and only when relevant. For example: if a fixed-rate discount is to be applied to certain items, then the rule is assumed to take responsibility for ensuring that those items are actually in the basket, and are of greater-than-zero quantity. This is according to the Single Responsibility principle.

* DiscountRuleFactory is only programmed to generate one instance per rule of all the rules at once. There doesn't seem to be any obvious scenario where applying only a single rule or only a sub-set of the rules would be necessary or useful, so I left such a mechanism out.

* Price is simply an attribute of Item. I can imagine scenarios where pricing might vary independent of particular items, while also not itself being a kind of discount. For example, we might want to add a rule that some percentage of inventory should have price A, and the rest have price B. Or that inventory at one location should have price A and the rest have price B. In that case, we might consider decoupling price from Item and creating a dynamic price calculation mechanism, such as an ItemPricer. However, given that the exercise only referred to prices varying by discount, I only built that mechanism.

* Item could have had other useful and important attributes. For example: category, supplier, etc. For simplicity, I left such attributes out.

* Entry could have had other useful and important attributes. For example: date added, customer id, etc. It also could have had defensive constraints. For example: quantity must be 1 or greater, or else, the entry simply shouldn't exist. For simplicity, I left such attributes and constraints out. 

* A Basket could have been modelled, to contain entries of items added. It could have enforced certain constraints defensively - e.g., only one entry per item. As per the guidelines given for this coding exercise, I avoided modelling a Shopping Basket.

