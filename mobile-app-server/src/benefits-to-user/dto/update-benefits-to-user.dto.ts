import { PartialType } from '@nestjs/mapped-types';
import { CreateBenefitsToUserDto } from './create-benefits-to-user.dto';

export class UpdateBenefitsToUserDto extends PartialType(CreateBenefitsToUserDto) {}
