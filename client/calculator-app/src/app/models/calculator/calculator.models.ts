export interface ICalculationRequestModel {
  definition: string;
  providerType: CalculationProviderType;
}

export interface ICalculationResponseModel {
  definition: string;
  result: string;
  providerType: CalculationProviderType;
  status: CalculationStatus;
  errorMessage?: string;
}

export enum CalculationProviderType {
  LocalProvider,
  MathJS,
  MathEval
}

export enum CalculationStatus {
  None,
  Completed,
  Failed
}

export function defaultCalculationResponseModel(): ICalculationResponseModel {
  return {
    definition: '',
    result: '',
    providerType: CalculationProviderType.LocalProvider,
    status: CalculationStatus.Completed
  };
}
