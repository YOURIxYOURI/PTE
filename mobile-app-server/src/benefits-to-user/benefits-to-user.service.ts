import { Injectable } from '@nestjs/common';
import { CreateBenefitsToUserDto } from './dto/create-benefits-to-user.dto';
import { UpdateBenefitsToUserDto } from './dto/update-benefits-to-user.dto';
import { PrismaService } from 'src/prisma.service';
import { BenefitsSearchDto } from './dto/benefits-search.dto';

@Injectable()
export class BenefitsToUserService {
  constructor(private prisma: PrismaService) {}
  create(createBenefitsToUserDto: CreateBenefitsToUserDto) {
    return 'This action adds a new benefitsToUser';
  }

  async findAll(data: BenefitsSearchDto) {
    const benefit = await this.prisma.benefitsToUser.findMany({
      where: {
        userId: data.userId,
      },
      include: {
        benefit: true,
      },
    });
    return benefit;
  }

  async findOne(data: BenefitsSearchDto) {
    const benefit = await this.prisma.benefitsToUser.findFirst({
      where: {
        benefitId: data.userId,
      },
      include: {
        benefit: true,
      },
    });
    return benefit;
  }

  update(id: number, updateBenefitsToUserDto: UpdateBenefitsToUserDto) {
    return `This action updates a #${id} benefitsToUser`;
  }

  remove(id: number) {
    return `This action removes a #${id} benefitsToUser`;
  }
}
