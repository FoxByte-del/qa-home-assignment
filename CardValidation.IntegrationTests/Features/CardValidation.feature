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

    Scenario: Empty input fields return error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            |||||
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "is required"

    Scenario: Empty owner input field returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            || 378282246310005  | 04/28     | 4411 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Owner is required"

    Scenario: Empty card number input field returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  || 04/28     | 741 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Number is required"

    Scenario: Empty date input field returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 4462030000000000 || 746 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Date is required"

    Scenario: Empty CVV input field returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 2222630000001125 | 04/28     ||
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Cvv is required"

    Scenario: Date in past returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 378282246310005  | 12/24     | 4411 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Wrong date"

    Scenario: Too long CVV returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 378282246310005  | 12/26     | 441187543 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Wrong cvv"

    Scenario: Too long owner name returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr Meow Snowball  | 378282246310005  | 12/26     | 4411 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Wrong owner"

    Scenario: Wrong card number returns error
        Given the following card data is inserted:
            | Owner      | CardNumber       | IssueDate | CVV |
            | Meow Purr  | 078282246310005  | 12/26     | 441 |
        When the card validation request is sent
        Then the response status code should be "400"
        And the response should contain "Wrong number"