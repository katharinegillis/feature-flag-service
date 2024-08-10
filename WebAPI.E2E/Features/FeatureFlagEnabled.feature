Feature: FeatureFlagEnabled

@sqlite
@splitio
Scenario: Get an enabled feature flag
	Given the feature flags exist
		| Id               | Enabled |
		| e2e_test_enabled | true    |
	When the feature flag enabled endpoint is opened for the e2e_test_enabled flag
	Then the result should be true