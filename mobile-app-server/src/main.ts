import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import * as os from 'os';

async function bootstrap() {
  const fs = require('fs');
  const app = await NestFactory.create(AppModule);
  const hostname = os.hostname();
  await app.listen(8080, hostname);
  const adress = await app.getUrl();
  console.log(adress);
}
bootstrap();
