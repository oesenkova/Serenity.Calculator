import {
  CalculationStatus,
  defaultCalculationResponseModel,
  ICalculationRequestModel,
  ICalculationResponseModel
} from "../../models/calculator/calculator.models";
import BaseApiService from "../base-api.service";
import {Injectable} from "@angular/core";

@Injectable()
class CalculatorService extends BaseApiService {
  private calculationResult: ICalculationResponseModel = defaultCalculationResponseModel();

  getCalculationResult(): ICalculationResponseModel {
    return this.calculationResult;
  }

  private setCalculationResult(data: ICalculationResponseModel) {
    let {definition, result, providerType, errorMessage} = data;

    this.calculationResult = {definition, result, providerType, status: CalculationStatus.Completed, errorMessage};
  }

  async calculate(request: ICalculationRequestModel) {
    await this.fetch({
      url: 'https://localhost:7024/api/expressions/calculate',
      method: 'POST',
      body: JSON.stringify(request)
    });

    if (this.response.ok){
      this.setCalculationResult(this.response.data);
    } else {
      this.calculationResult.status = CalculationStatus.Failed;
      this.calculationResult.errorMessage = this.response.data.title;
    }
  }
}

export default CalculatorService;
