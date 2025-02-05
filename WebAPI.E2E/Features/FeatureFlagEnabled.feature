Feature: FeatureFlagEnabled

@sqlite
@splitio
Scenario: Get an enabled feature flag
    Given the testing feature flags exist
    When the feature flag enabled endpoint is opened for the enabled test flag
    Then the result should be true