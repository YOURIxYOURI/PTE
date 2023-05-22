import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
} from '@nestjs/common';
import { BenefitsToUserService } from './benefits-to-user.service';
import { CreateBenefitsToUserDto } from './dto/create-benefits-to-user.dto';
import { UpdateBenefitsToUserDto } from './dto/update-benefits-to-user.dto';
import { BenefitsSearchDto } from './dto/benefits-search.dto';

@Controller('benefits-to-user')
export class BenefitsToUserController {
  constructor(private readonly benefitsToUserService: BenefitsToUserService) {}

  @Post()
  create(@Body() createBenefitsToUserDto: CreateBenefitsToUserDto) {
    return this.benefitsToUserService.create(createBenefitsToUserDto);
  }

  @Post('benefits')
  findAll(@Body() user: BenefitsSearchDto) {
    return this.benefitsToUserService.findAll(user);
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.benefitsToUserService.findOne(+id);
  }

  @Patch(':id')
  update(
    @Param('id') id: string,
    @Body() updateBenefitsToUserDto: UpdateBenefitsToUserDto,
  ) {
    return this.benefitsToUserService.update(+id, updateBenefitsToUserDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.benefitsToUserService.remove(+id);
  }
}
