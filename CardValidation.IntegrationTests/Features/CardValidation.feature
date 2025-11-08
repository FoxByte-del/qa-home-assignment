Feature: Validate credit card data

API that validates credit card data.

    Scenario: Valid Visa card returns Visa as card type
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 4462030000000000 | 04/28     | 746 |
        When the card validation request is sent
        Then the response status code should be "200"
        And the response should contain "10"