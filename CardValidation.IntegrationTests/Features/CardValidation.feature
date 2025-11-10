Feature: Validate credit card data

API that validates credit card data.

    Scenario: Valid Visa card returns proper card type
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 4462030000000000 | 04/28     | 746 |
        When the card validation request is sent
        Then the response status code should be "200"
        And the response should contain "10"

    Scenario: Valid Mastercard card returns proper card type
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 2222630000001125 | 04/28     | 741 |
        When the card validation request is sent
        Then the response status code should be "200"
        And the response should contain "20"

    Scenario: Valid American Express card returns proper card type
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 378282246310005  | 04/28     | 4411 |
        When the card validation request is sent
        Then the response status code should be "200"
        And the response should contain "30"