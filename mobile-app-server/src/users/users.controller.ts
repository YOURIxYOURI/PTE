import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
} from '@nestjs/common';
import { UsersService } from './users.service';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { UserEmailDto } from './dto/user.dto';

@Controller('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Post()
  create(@Body() createUserDto: CreateUserDto) {
    return this.usersService.create(createUserDto);
  }

  @Get()
  findAll() {
    return this.usersService.findAll();
  }

  @Post('data')
  findOne(@Body() user: UserEmailDto) {
    return this.usersService.findOne(user.email);
  }
  @Post('findUser')
  findUser(@Body() user: UpdateUserDto) {
    return this.usersService.findUser(user);
  }

  @Post('login')
  findOneLogin(@Body() user: UpdateUserDto) {
    return this.usersService.findOneLogin(user);
  }

  @Patch('register')
  update(@Body() updateUserDto: UpdateUserDto) {
    return this.usersService.update(updateUserDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.usersService.remove(+id);
  }
}
