Feature: FeatureFlagEnabled

    @SQLite
    @Split
    @E2E
    Scenario: Get an enabled feature flag
        Given the following feature flags exist
          | Id               | Enabled |
          | e2e_test_enabled | true    |
        When the feature flag enabled endpoint is opened for the e2e_test_enabled feature flag
        Then the result should be true

    @SQLite
    @Split
    @E2E
    Scenario: Get a disabled feature flag
        Given the following feature flags exist
          | Id                | Enabled |
          | e2e_test_disabled | false   |
        When the feature flag enabled endpoint is opened for the e2e_test_disabled feature flag
        Then the result should be false