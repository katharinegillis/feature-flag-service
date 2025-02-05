Feature: FeatureFlagEnabled

@sqlite
@splitio
Scenario: Get an enabled feature flag
    Given the testing feature flags exist
    When the feature flag enabled endpoint is opened for the enabled test flag
    Then the result should be true
    
@sqlite
@splitio
Scenario: Get a disabled feature flag
    Given the testing feature flags exist
    When the feature flag enabled endpoint is opened for the disabled test flag
    Then the result should be false