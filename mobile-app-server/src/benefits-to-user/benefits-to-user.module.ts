import { Module } from '@nestjs/common';
import { BenefitsToUserService } from './benefits-to-user.service';
import { BenefitsToUserController } from './benefits-to-user.controller';
import { PrismaService } from 'src/prisma.service';

@Module({
  controllers: [BenefitsToUserController],
  providers: [BenefitsToUserService, PrismaService],
})
export class BenefitsToUserModule {}
