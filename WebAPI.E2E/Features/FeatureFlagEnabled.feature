Feature: FeatureFlagEnabled
            
    @SQLite
    @Split
    @E2E
    Scenario: Get an enabled feature flag with v1
        Given the following feature flags exist
          | Id               | Enabled |
          | e2e_test_enabled | true    |
        When the v1 feature flag enabled endpoint is opened for the e2e_test_enabled feature flag
        Then the result should be successful and true

    @SQLite
    @Split
    @E2E
    Scenario: Get a disabled feature flag with v1
        Given the following feature flags exist
          | Id                | Enabled |
          | e2e_test_disabled | false   |
        When the v1 feature flag enabled endpoint is opened for the e2e_test_disabled feature flag
        Then the result should be successful and false
        
    @SQLite
    @Split
    @E2E
    Scenario: Get a non-existant feature flag with v1
        When the v1 feature flag enabled endpoint is opened for the e2e_test_missing feature flag
        Then the result should be unsuccessful with the following errors
          | Not found |