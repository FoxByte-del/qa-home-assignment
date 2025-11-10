Feature: Validate credit card data
  The API validates credit card data and returns card type or errors.

  Scenario Outline: Valid card returns proper card type
    Given the following card data is inserted:
      | Owner     | CardNumber       | IssueDate | CVV |
      | Meow Purr | <number>         | 04/28     | <cvv> |
    When the card validation request is sent
    Then the response status code should be "200"
    And the response should contain "<result>"

    Examples:
      | number             | cvv  | result |
      | 4462030000000000   | 746  | 10     |
      | 2222630000001125   | 741  | 20     |
      | 378282246310005    | 4411 | 30     |

  Scenario Outline: Missing required field returns error
    Given the following card data is inserted:
      | Owner     | CardNumber   | IssueDate | CVV |
      | <owner>   | <number>     | <date>    | <cvv> |
    When the card validation request is sent
    Then the response status code should be "400"
    And the response should contain "<error>"

    Examples:
      | owner       | number            | date   | cvv    | error                  |
      |             |                   |        |        | is required            |
      |             | 378282246310005   | 04/28  | 4411   | Owner is required      |
      | Meow Purr   |                   | 04/28  | 741    | Number is required     |
      | Meow Purr   | 4462030000000000  |        | 746    | Date is required       |
      | Meow Purr   | 2222630000001125  | 04/28  |        | Cvv is required        |

  Scenario Outline: Invalid card data returns domain error
    Given the following card data is inserted:
      | Owner     | CardNumber       | IssueDate | CVV |
      | <owner>   | <number>         | <date>    | <cvv> |
    When the card validation request is sent
    Then the response status code should be "400"
    And the response should contain "<error>"

    Examples:
      | owner                  | number             | date   | cvv    | error          |
      | Meow Purr              | 078282246310005    | 12/26  | 441    | Wrong number   |
      | Meow Purr              | 378282246310005    | 12/24  | 4411   | Wrong date     |
      | Meow Purr              | 378282246310005    | 12/26  | 441187 | Wrong cvv      |
      | Meow Purr Meow Snowball| 378282246310005    | 12/26  | 4411   | Wrong owner    |
