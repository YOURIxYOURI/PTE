import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { UsersModule } from './users/users.module';
import { Prisma } from '@prisma/client';
import { PrismaService } from './prisma.service';
import { BenefitsToUserModule } from './benefits-to-user/benefits-to-user.module';

@Module({
  imports: [UsersModule, BenefitsToUserModule],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
