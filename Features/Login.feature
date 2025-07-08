Feature: Login functionality
    As a user
    I want to log in to demoqa.com
    So that I can access my profile

    Scenario: Successful login
        Given I navigate to the login page
        When I enter valid credentials
        Then I should see my profile page
